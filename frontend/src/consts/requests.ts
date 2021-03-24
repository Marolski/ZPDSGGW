import axios from "axios";
require('dotenv');

export default class Requests{

        API_URL = process.env.VUE_APP_API_URL;
        config = {
            headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
        };
        

    public async get(endpoint = '', param = ''): Promise<any>{
        return param? await axios.get(`${this.API_URL}/${endpoint}/${param}`,this.config)
         : axios.get(`${this.API_URL}/${endpoint}`,this.config);
    }

    public async getStringParam(endpoint = '', param = ''): Promise<any>{
        return param? await axios.get(`${this.API_URL}/${endpoint}${param}`,this.config)
         : axios.get(`${this.API_URL}/${endpoint}`, this.config);
    }
         
    public async post(endpoint = '', param: object): Promise<any>{
        return await axios.post(`${this.API_URL}/${endpoint}`, param,this.config);
    }

    public async delete(endpoint = '', param = ''): Promise<any>{
        return await axios.delete(`${this.API_URL}/${endpoint}/${param}`,this.config);
    }

    public async put(endpoint = '', param = '', body: object): Promise<any>{
        return await axios.put(`${this.API_URL}/${endpoint}/${param}`,body,this.config)
    }

    public async patch(endpoint = '', param = '', body: Array<object>): Promise<any>{
        return await axios.patch(`${this.API_URL}/${endpoint}/${param}`, body, this.config)
    }
}
