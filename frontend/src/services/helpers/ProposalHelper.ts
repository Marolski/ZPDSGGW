import IProposal from '../../types/Proposal';
import ProposalService from '../ProposalService';

const proposalService = new ProposalService();
export default class ProposalHelper{
    async createProposalWithPromotorId(idStudent: string, idPromoter: string){
        const emptyProposal: IProposal = {
            Status : 1,
            Topic: '',
            PromoterId : idPromoter,
            StudentId : idStudent
        }
        await proposalService.postProposal(emptyProposal);
    }
}