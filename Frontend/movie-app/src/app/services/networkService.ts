import axios from "axios";
import IErrorResponse from "../models/IErrorResponse";
import INetworkResponse from "../models/INetworkResponse";
import { environment } from "../../environments/environment";

class NetworkService {

    private api: any;

    constructor() {
        const baseUrl = environment.apiBaseUrl;
        this.api = axios.create({
            baseURL: baseUrl,
            timeout: 30_000
        });

      
        this.api.interceptors.request.use(async (config: any) => {
            const token = this.getAccessToken();
            
            if (token) {
                config.headers["Authorization"] = `Bearer ${token}`;
            }

            return config;
        }, (error: any) => Promise.reject(error));

 
        this.api.interceptors.response.use(
            (response: any) => response,
            (error: any) => {
                if (error.response?.status === 401) {
                    
                    this.clearAuthData();
                
                }
                return Promise.reject(error);
            }
        );
    }


    private getAccessToken(): string | null {
    if (typeof localStorage === "undefined") {
        return null; 
    }
    return localStorage.getItem('auth_token');
}

  
    private clearAuthData(): void {
    if (typeof localStorage === "undefined") {
        return; 
    }
    localStorage.removeItem('auth_token');
    localStorage.removeItem('auth_user');
}


    public async get<T>(path: string): Promise<INetworkResponse<T>> {
        try {
            const response = await this.api.get(path);
            return this.createNetworkResponse<T>(response);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                throw error.response;
            } else {
                throw error;
            }
        }
    }


    public async post<TBody, TResponse>(path: string, body?: TBody, headers?: any): Promise<INetworkResponse<TResponse>> {
        let contentType: string;
        
        if (typeof body === "string") {
            contentType = "text/plain";
        } else if (body instanceof FormData) {
            contentType = "multipart/form-data";
        } else if (typeof body === "number") {
            contentType = "application/json";
        } else {
            contentType = "application/json";
        }

        headers = headers || {};
        if (contentType !== "multipart/form-data") {
            headers["Content-Type"] = contentType;
        }
        
        try {
            const response = await this.api.post(path, body, { headers });
            return this.createNetworkResponse<TResponse>(response);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                throw error.response;
            } else {
                throw error;
            }
        }
    }


    public async put<TBody, TResponse>(path: string, body?: TBody): Promise<INetworkResponse<TResponse>> {
        let contentType: string | undefined;
        
        if (typeof body === "string") {
            contentType = "text/plain";
        } else if (body instanceof FormData) {
            contentType = undefined;
        } else {
            contentType = "application/json";
        }
        
        try {
            const response = await this.api.put(path, body, { headers: { "Content-Type": contentType } });
            return this.createNetworkResponse<TResponse>(response);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                throw error.response;
            } else {
                throw error;
            }
        }
    }

    
    private isRequestSuccessful = (status: number) => status >= 200 && status < 300;

    private createNetworkResponse = <T>(response: any): INetworkResponse<T> => {
        const networkResponse: INetworkResponse<T> = {
            status: response.status,
            isSuccessful: this.isRequestSuccessful(response.status)
        };

        if (!response.data && response.data !== 0) {
            return networkResponse;
        }

        if (networkResponse.isSuccessful) {
            networkResponse.data = response.data as T;
        } else {
            networkResponse.data = response.data as IErrorResponse;
        }

        return networkResponse;
    }
}

const networkService = new NetworkService();
export default networkService;