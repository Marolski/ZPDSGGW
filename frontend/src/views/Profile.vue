<script lang='ts'>
    import { Component, Vue } from 'vue-property-decorator';
    import IUser from "../types/User";
    import UserService from "../services/UserService"

    const userService = new UserService();
    @Component({
        name: 'MyProfile',
        components:{}
    })
    export default class MyProfile extends Vue{
        //data
        myProfile: IUser = {
            Id: '',
            name: '',
            surname: '',
            studentNumber: '',
            degrees: 0,
            availability: 0,
            role: '',

        };
        tmp: any = 'aaaa'

        //computed properties
        get userCount(){
            return this.myProfile;
        }
        //props
        //methods
        //lifecycles hooks
        created(){
            this.test()
            console.log(this.myProfile);
        }

       
        async test() {
            this.myProfile = await userService.getUser();
        }
        //watchers
    }

</script>


<template>
  <div class="about">
    <h1>Moj profil</h1>
    <div>
        <div>
            Imie: {{myProfile}}
        </div>
        <div>
            Nazwisko:{{myProfile.Surname}}
        </div>
        <div>
            Numer indeksu: {{myProfile.studentNumber}}
        </div>
        <button v-on:click="send">
        </button>
    </div>
  </div>
</template>
