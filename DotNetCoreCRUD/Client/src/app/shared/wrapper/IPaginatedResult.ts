export interface IPaginatedResult<T>{
  pageNumber: number,
  pageSize: number,
  count: number,
  totalPages: number,
  succeeded: boolean,
  message: string | null,
  errors: null,
  data:T[]
}
