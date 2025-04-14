import { HttpInterceptorFn, HttpResponse } from "@angular/common/http";
import { map } from "rxjs";

// API’den dönen tarihleri `Date` nesnesine çevirmek için interceptor
export const dateConversionInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    map(response => {
      if (response instanceof HttpResponse && response.body) {
        return response.clone({ body: convertDates(response.body) });
      }
      return response;
    })
  );
};

// API’den gelen verinin içindeki tarihleri `Date` nesnesine çeviren fonksiyon
function convertDates<T>(data: T): T {
  if (typeof data !== "object" || data === null) return data;

  return Array.isArray(data)
    ? (data.map(item => convertDates(item)) as T) // Eğer dizi ise, her elemanı dönüştür
    : (Object.fromEntries(
        Object.entries(data as Record<string, unknown>).map(([key, value]) => {
          if (isISODate(value)) {
            return [key, new Date(value as string | number)]; // Tarih formatındaysa, Date nesnesine çevir
          } else if (typeof value === "object" && value !== null) {
            return [key, convertDates(value)]; // İç içe nesneleri de kontrol et
          }
          return [key, value];
        })
      ) as T);
}

// String'in ISO 8601 tarih formatında olup olmadığını kontrol eden fonksiyon
function isISODate(value: any): boolean {
  return (
    typeof value === "string" &&
    /^\d{4}-\d{2}-\d{2}(T\d{2}:\d{2}:\d{2}.*)?$/.test(value) // Zaman kısmı opsiyonel
  );
}
