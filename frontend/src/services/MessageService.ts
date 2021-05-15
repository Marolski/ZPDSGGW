import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
import IMessage from '@/types/IMessage';
require('dotenv');

const request = new Request();
export default class MessageService{
    getAllRecivierMessage = (id: string) => request.getFileList(endpoints.allMessage, id)
    getMessage = (id: string) => request.get(endpoints.message, id)
    getMessageFile = (id: string) => request.getFileList(endpoints.message, id)
    postMessage = (file: object, sendTo: string, sendFrom: string, description: string) => request.postMessage(endpoints.message,file,sendTo,sendFrom,description);
}