import { HttpStatusCode } from "axios";


export default interface IErrorResponse {

    type: string;
    title: string;
    status: HttpStatusCode;
    traceId: string;
}