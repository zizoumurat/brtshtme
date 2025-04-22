import { InjectionToken } from '@angular/core';
import { DiscountModel } from '@/core/models/crm/discount.model';
import { ICrudService   } from '../admin/crud-service';

export interface IDiscountService extends ICrudService<DiscountModel> {}

export const DISCOUNT_SERVICE = new InjectionToken<IDiscountService>('DiscountService');