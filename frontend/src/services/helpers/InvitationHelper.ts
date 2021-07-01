import InvitationService from '../InvitationService';
import ProposalService from '../ProposalService';
import ThesisTopicService from '../ThesisTopicService';
import {InvitationStatus, ThesisTopicStatus} from "../../enums/Enum";

const topics = new ThesisTopicService();
const proposalService = new ProposalService();
const invitationservice = new InvitationService();
export default class InvitationHelper{
    userId: any = localStorage.getItem('id');

    async updateInvitationStatus(checkedTopicId: string){
        const invitationExist = await invitationservice.getInvitation(this.userId)
        if(invitationExist.data.Accepted==InvitationStatus.InProgress || invitationExist.data.Accepted==InvitationStatus.Rejected){
            if(checkedTopicId!='')
                await topics.patchTopic(checkedTopicId,[{ "op":"replace", "path":"/Available", "value": ThesisTopicStatus.reserved}])

            await invitationservice.patchInvitation(this.userId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Send}]);
            return false;
        }
        else return true;
      }
}