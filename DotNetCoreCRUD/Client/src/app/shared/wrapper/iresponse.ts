export interface IResponse<T> {
    succeeded: boolean,
    message: string,
    errors: null | any,
    data: T | T[]
  }