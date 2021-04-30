import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
import IInvitation from '@/types/Invitation';

const request = new Request();
export default class DocumentService{
    uploadDocument = (documentKind: number,accepted: boolean,userId: string, file: object) => request.upload(endpoints.uploadFile, documentKind,accepted,userId, file);
}
