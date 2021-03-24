import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class ProposalService{
    getProposal = (id: string) => request.get(enpoints.proposal, id)
    patchProposal = (id: string, body: Array<object>) => request.patch(endpoints.proposal,id,body)
    postProposal = (body: object) => request.post(endpoints.proposal, body)
}