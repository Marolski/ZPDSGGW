import UserService from '../UserService';


const userService = new UserService();
export default class UserHelper{
    async getUserName(id: string){
        const user = await userService.getUser(id);
        if(user.data.Role == 'Promoter')
            return `${user.data.Degrees} ${user.data.Name} ${user.data.Surname}`
        else return `${user.data.Name} ${user.data.Surname}`
    }
}