import { HttpStatusCode } from "axios";
import IErrorResponse from "./IErrorResponse";


export default interface INetworkResponse<T> {

 
    status: HttpStatusCode;
    isSuccessful: boolean;
    data?: T | IErrorResponse;
}