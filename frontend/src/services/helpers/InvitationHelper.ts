import IProposal from '@/types/Proposal';
import IInvitation from '../../types/Invitation';
import InvitationService from '../InvitationService';
import ProposalService from '../ProposalService';
import ThesisTopicService from '../ThesisTopicService';
import ProposalHelper from './ProposalHelper';

const topics = new ThesisTopicService();
const proposalService = new ProposalService();
const invitationservice = new InvitationService();
export default class InvitationHelper{
    userId: any = localStorage.getItem('id');
    proposal: IProposal = {
        Status:1,
        StudentId: '',
        PromoterId: '',
        Topic: ''
    };
    async postInvitationIfNotExist(checkedTopicId: string, invitation: IInvitation){
        const invitationExist = await invitationservice.getInvitation(this.userId)
        console.log(invitationExist.data)
        //sprawdzam czy zaproszenie juz istanieje
        if(invitationExist.data==""){
            //jesli nie istnieje to tworze
            /////jesli jest id tematu czyli temat z listy to aktualizacja dostępności tematu na zarezerwowany
            if(checkedTopicId!='')
                await topics.patchTopic(checkedTopicId,[{ "op":"replace", "path":"/Available", "value": 3}]) 
            
            //sprawdzenie czy istnieje wniosek
            const proposalData = await proposalService.getProposal(this.userId);
            if(proposalData.data==""){
                this.proposal.PromoterId = invitation.PromoterId;
                this.proposal.StudentId = invitation.StudentId;
                this.proposal.Topic = invitation.Topic;
                proposalService.postProposal(this.proposal);
            }
            await invitationservice.postInvitation(invitation);
        }
        else{
            return true;
        }
        return false;
      }
}