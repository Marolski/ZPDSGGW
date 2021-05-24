import Request from '../consts/requests';
import endpoints from '../consts/endpoints';

const request = new Request();
export default class DocumentService{
    uploadDocument = (documentKind: number,accepted: boolean,userId: string, file: object) => request.upload(endpoints.uploadFile, documentKind,accepted,userId, file);
    getDocumentByUserId = (userId: string) => request.get(endpoints.uploadFile,userId);
    getDocumentList = (userId: string) => request.getFileList(endpoints.uploadFile,userId);
    deleteFile =(id: string) => request.delete(endpoints.uploadFile,id);
    patchDocument = (id: string, body: object[]) => request.patch(endpoints.uploadFile,id,body);
}
