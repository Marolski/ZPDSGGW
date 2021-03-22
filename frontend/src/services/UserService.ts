import Request from '../consts/requests';
import endpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class UserService{
        getUser =(id: string) => request.get(endpoints.user, id);
        getAllUsers =(role: string)=>request.getStringParam(endpoints.users, role);
        patchUser = (id: string, body: object) => request.patch(endpoints.user,id, body)
}