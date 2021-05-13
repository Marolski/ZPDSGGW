import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
import IMessage from '@/types/IMessage';
require('dotenv');

const request = new Request();
export default class MessageService{
    getAllRecivierMessage = (id: string) => request.get(endpoints.message, id)
    getMessage = (id: string) => request.get(endpoints.message, id)
    postMessage = (file: object, body: IMessage) => request.postMessage(endpoints.message, file, body);
}