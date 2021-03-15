import Request from '../consts/requests';
import enpoints from '../consts/endpoints';
import endpoints from '../consts/endpoints';
require('dotenv');

const request = new Request();
export default class ThesisTopicService{
    getTopics = () => request.get(endpoints.thesisTopic);
}