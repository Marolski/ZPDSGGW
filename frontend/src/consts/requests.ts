import axios from "axios";
require('dotenv');

export default class Requests{

        API_URL = process.env.VUE_APP_API_URL;

    public async get(endpoint = '', param = ''): Promise<any>{
        console.log(this.API_URL);
        console.log(endpoint);
        console.log(`${this.API_URL}/${endpoint}/${param}`);
        return param? await axios.get(`${this.API_URL}/${endpoint}/${param}`)
         : axios.get(`${this.API_URL}/${endpoint}`);
    }
         
    public async post(endpoint = '', param = ''): Promise<any>{
        return param? await axios.post(`${this.API_URL}/${endpoint}/${param}`)
         : axios.post(`${this.API_URL}/${endpoint}`);
    }

    public async delete(endpoint = '', param = ''): Promise<any>{
        return param? await axios.delete(`${this.API_URL}/${endpoint}/${param}`)
         : axios.delete(`${this.API_URL}/${endpoint}`);
    }

    public async put(endpoint = '', param = ''): Promise<any>{
        return param? await axios.put(`${this.API_URL}/${endpoint}/${param}`)
         : axios.put(`${this.API_URL}/${endpoint}`);
    }

    public async patch(endpoint = '', param = ''): Promise<any>{
        return param? await axios.patch(`${this.API_URL}/${endpoint}/${param}`)
         : axios.patch(`${this.API_URL}/${endpoint}`);
    }
}
