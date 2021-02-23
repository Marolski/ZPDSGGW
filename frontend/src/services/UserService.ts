import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class UserService{
        getUser =() => request.get(enpoints.user);
}