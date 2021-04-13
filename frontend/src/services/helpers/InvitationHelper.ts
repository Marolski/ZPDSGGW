import IInvitation from '../../types/Invitation';
import InvitationService from '../InvitationService';
import ProposalService from '../ProposalService';
import ThesisTopicService from '../ThesisTopicService';

const topics = new ThesisTopicService();
const proposalService = new ProposalService();
const invitationservice = new InvitationService();
export default class InvitationHelper{
    userId: any = localStorage.getItem('id');
    async postInvitation(checkedTopicId: string, invitation: IInvitation, invitationDesc: string){
        if(checkedTopicId!='')
            await topics.patchTopic(checkedTopicId,[{ "op":"replace", "path":"/Available", "value": 3}])
        invitation.StudentId = this.userId;
        const proposal = await proposalService.getProposal(this.userId);
        const proposaldata = proposal.data;
        invitation.PromoterId = proposaldata.PromoterId
        invitation.Topic = proposaldata.Topic;
        invitation.Description = invitationDesc;
        invitation.Accepted = false;
        await invitationservice.postInvitation(invitation);

      }
}