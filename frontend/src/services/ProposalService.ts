import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class ProposalService{
    getProposal = () => request.get(enpoints.proposal, '60758688-3B61-4C74-B755-12179DF8A524')
}