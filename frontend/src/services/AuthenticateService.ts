import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class Authenticate{
    authorization = (obj) => request.post(enpoints.auth, obj);
}