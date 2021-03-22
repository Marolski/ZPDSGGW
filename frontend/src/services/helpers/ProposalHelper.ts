import IProposal from '../../types/Proposal';
import ProposalService from '../ProposalService';
import Guid from './GuidGenerator';


const guid = new Guid();
const proposalService = new ProposalService();
export default class ProposalHelper{
    async createEmptyProposal(id: string){
        const emptyProposal: IProposal = {
            Id : guid.generateUUID(),
            Status : 1,
            Topic: '',
            Date: Date.UTC,
            PromoterId : '',
            StudentId : id
        }
        await proposalService.postProposal(emptyProposal);
    }
}