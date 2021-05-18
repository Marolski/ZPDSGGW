import IProposal from '@/types/Proposal';
import IInvitation from '../../types/Invitation';
import InvitationService from '../InvitationService';
import ProposalService from '../ProposalService';
import ThesisTopicService from '../ThesisTopicService';
import {InvitationStatus, ThesisTopicStatus} from "../../enums/Enum";

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
    async updateInvitationStatus(checkedTopicId: string){
        const invitationExist = await invitationservice.getInvitation(this.userId)
        console.log(invitationExist.data)
        console.log(InvitationStatus.InProgress)
        if(checkedTopicId == ''){
            await invitationservice.patchInvitation(this.userId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Send}]);
            return false;
        }
        if(invitationExist.data.Accepted==InvitationStatus.InProgress){
            if(checkedTopicId!='')
                await topics.patchTopic(checkedTopicId,[{ "op":"replace", "path":"/Available", "value": ThesisTopicStatus.reserved}])

            await invitationservice.patchInvitation(this.userId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Send}]);
            return false;
        }
        else return true;
      }
}