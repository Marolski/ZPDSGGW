import axios from "axios";
require('dotenv');

export default class Requests{

        Token: string|null = '';
        API_URL = process.env.VUE_APP_API_URL;
        config = {
            headers: { Authorization: `Bearer ${localStorage.getItem('token')}`, "Access-Control-Allow-Origin": "*" }
        };
        configUpload = {
            headers: { 'content-type': 'multipart/form-data', Authorization: `Bearer ${localStorage.getItem('token')}`,
            "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, DELETE", 'Access-Control-Allow-Headers': 'X-Requested-With', "Access-Control-Allow-Origin": "*"
         }
        }
    public async createHeaderWithToken(){
        this.Token = localStorage.getItem('token');
    }   

    public async get(endpoint = '', param = ''): Promise<any>{
        await this.createHeaderWithToken();
        return param? await axios.get(`${this.API_URL}/${endpoint}/${param}`,this.config)
         : axios.get(`${this.API_URL}/${endpoint}`,this.config);
    }

    public async getStringParam(endpoint = '', param = ''): Promise<any>{
        await this.createHeaderWithToken();
        return param? await axios.get(`${this.API_URL}/${endpoint}${param}`,this.config)
         : axios.get(`${this.API_URL}/${endpoint}`, this.config);
    }
         
    public async post(endpoint = '', param: object): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.post(`${this.API_URL}/${endpoint}`, param,this.config);
    }

    public async delete(endpoint = '', param = ''): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.delete(`${this.API_URL}/${endpoint}/${param}`,this.config);
    }

    public async put(endpoint = '', param = '', body: object): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.put(`${this.API_URL}/${endpoint}/${param}`,body,this.config)
    }

    public async patch(endpoint = '', param = '', body: Array<object>): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.patch(`${this.API_URL}/${endpoint}/${param}`, body, this.config)
    }
    public async upload(endpoint = '', documentKind: string,accepted: boolean,userId: string, body: object): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.post(`${this.API_URL}/${endpoint}?documentKind=${documentKind}&accepted=${accepted}&userId=${userId}`,body,this.configUpload)
    }
    public async getFileList(endpoint = '', param = ''): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.get(`${this.API_URL}/${endpoint}?id=${param}`, this.config)
    }
    public async postMessage(endpoint = '', file: object, sendTo: string, sendFrom: string, description: string): Promise<any>{
        await this.createHeaderWithToken();
        return await axios.post(`${this.API_URL}/${endpoint}?sendFrom=${sendFrom}&sendTo=${sendTo}&description=${description}`, file, this.configUpload)
    }
}
