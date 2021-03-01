<script lang='ts'>
    import IUser from "../types/User";
    import UserService from "../services/UserService";
    import Vue from 'vue';
    import { Component } from "vue-property-decorator";

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
            const tmp= await userService.getUser();
            console.log(tmp.data);
            this.myProfile = tmp.data;
        }
        //watchers
    }

</script>

<template>
  <div class="about">
    <h1>Moj profil</h1>
    <div>
        <div>
            Imie: {{myProfile.Name}}
        </div>
        <div>
            Nazwisko:{{myProfile.Surname}}
        </div>
        <div>
            Numer indeksu: {{myProfile.StudentNumber}}
        </div>
        <div>
            <md-button class="md-primary md-raised">Primary</md-button>
        </div>
    </div>
  </div>
</template>
