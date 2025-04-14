import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { ICrudService } from '@/core/services/admin/crud-service';
import { Component, inject, InjectionToken } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { TableLazyLoadEvent } from 'primeng/table';
import { debounceTime, Subject } from 'rxjs';

@Component({
    selector: 'app-base',
    template: '',
    styleUrls: []
})
export class AppBaseComponent<T extends HasId, S extends ICrudService<T>> {
    protected recordService: S;

    pageForm!: FormGroup;
    rows: Array<T & HasId> = [];
    totalRecords: number = 0;
    maxPageRow: number = 20;
    loading: boolean = true;
    displayModal: boolean = false;
    lastLazyLoadEvent: TableLazyLoadEvent | undefined;
    pageTitle: string = '';
    pageModal: string = '';
    private searchInputSubject = new Subject<string>();
    searchFilter: Partial<Record<string, any>> = {};

    currentPageUrl: string = '';

    constructor(serviceToken: InjectionToken<S>) {
        this.recordService = inject(serviceToken);
    }

    protected setPageUrl(url: string) {
        this.currentPageUrl = url;
    }

    ngOnInit() {
        this.searchInputSubject.pipe(debounceTime(300)).subscribe((searchTerm) => {
            this.loadData();
        });
    }

    protected async loadData(event?: TableLazyLoadEvent) {
        if (!event) {
            event = this.lastLazyLoadEvent;
        }

        this.loading = true;
        const paginationFilter = new PaginationFilterModel();

        if (event) {
            this.lastLazyLoadEvent = event;

            paginationFilter.page = (event.first ?? 0) / (event.rows || this.maxPageRow);
            paginationFilter.pageSize = event.rows || this.maxPageRow;

            paginationFilter.sortByMultiName = event.sortField ? [event.sortField.toString()] : ['Id'];
            paginationFilter.sortByMultiOrder = event.sortOrder !== undefined ? [event.sortOrder ?? 0] : [0];
        }

        try {
            const result = await this.recordService.getAll(paginationFilter, this.searchFilter);
            this.totalRecords = result.count;
            this.rows = result.items as unknown as T[];
        } catch (error) {
            console.error("Veri yüklenirken hata oluştu:", error);
        }

        this.loading = false;
    }

    handleEdit(id: string) {
        var data = this.rows.find(x => x.id === id);

        if (!data) return;

        this.pageForm.patchValue(data);
        this.pageModal = "Düzenle";
        this.displayModal = true;
    }

    onSearch(value: any, field: string, isDate: boolean = false) {
        if (value === null || value === undefined || value === '') {
            const { [field]: _, ...updatedFilter } = this.searchFilter;
            this.searchFilter = updatedFilter;
        } else {
            const formattedValue = isDate ? this.formatDate(value) : value;
            this.searchFilter = { ...this.searchFilter, [field]: formattedValue };
        }

        this.searchInputSubject.next(JSON.stringify(this.searchFilter));
    }

    formatDate(date: Date): string {
        if (!date) return '';
        const d = new Date(date);

        const year = d.getFullYear();
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const day = String(d.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    openModal() {
        this.pageForm.reset();
        this.pageModal = "Ekle";

        this.displayModal = true;
    }

    closeModal() {
        this.displayModal = false;
    }

    deleteRecord(recordId: number) {
        this.recordService.delete(recordId).then(() => {
            this.loadData(this.lastLazyLoadEvent);
        });
    }

    async saveRecord() {

        if (this.pageForm.invalid) {
            this.pageForm.markAllAsTouched();

            return;
        };

        if (this.pageForm.value.id) {
            await this.recordService.update(this.pageForm.value);
        }
        else {
            await this.recordService.create(this.pageForm.value);
        }

        this.loadData(this.lastLazyLoadEvent);
        this.displayModal = false;
        this.pageForm.reset();
    }

    isFieldInvalid(controlName: string): boolean {
        const control = this.pageForm.get(controlName);
        return control?.invalid && control?.touched ? true : false;
    }

}
