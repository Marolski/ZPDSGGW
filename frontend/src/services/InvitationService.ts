import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
import IInvitation from '@/types/Invitation';
require('dotenv');

const request = new Request();
export default class InvitationService{
    getAllInvitations = () => request.get(endpoints.invitation)
    getInvitation = (id: string) => request.get(endpoints.invitation, id)
    postInvitation = (body: IInvitation) => request.post(endpoints.invitation, body);
    patchInvitation = (id: string, body: Array<object>) => request.patch(endpoints.invitation,id,body);
}