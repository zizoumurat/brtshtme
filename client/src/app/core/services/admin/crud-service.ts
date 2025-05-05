import { SelectListItem } from '@/core/models/select-list-item.model';
import { PaginationFilterModel } from '../../models/admin/paginationFilterModel';
import { PaginationResponseModel } from '../../models/admin/paginationResponseModel';

export interface ICrudService<T extends HasId> {
    getAll(filter: PaginationFilterModel, search?: any): Promise<PaginationResponseModel<T>>;
    create(item: T): Promise<any>;
    update(item: T): Promise<void>;
    delete(ref: number): Promise<void>;
    getSelectList(...params: (string | number)[]): Promise<SelectListItem[]>;
}
