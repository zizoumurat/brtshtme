import { PaginationFilterModel } from '../../models/admin/paginationFilterModel';
import { PaginationResponseModel } from '../../models/admin/paginationResponseModel';

export interface ICrudService<T extends HasId> {
    getAll(filter: PaginationFilterModel, search?: any): Promise<PaginationResponseModel<T>>;
    create(item: T): Promise<void>;
    update(item: T): Promise<void>;
    delete(ref: number): Promise<void>;
}
