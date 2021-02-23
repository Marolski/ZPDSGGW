<template>
    <div class="vue-tempalte">
        <form>
            <h3>Sign In</h3>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" class="form-control form-control-lg" />
            </div>

            <button type="submit" class="btn btn-dark btn-lg btn-block" v-on:click="login">Sign In</button>

            <p class="forgot-password text-right mt-2 mb-4">
                <router-link to="/forgot-password">Forgot password ?</router-link>
            </p>

        </form>
    </div>
</template>

<script lang='ts'>
    import { Component, Vue } from 'vue-property-decorator';
    @Component({
        name: 'Login',
        components:{}
    })
    export default class Login extends Vue{
        //data

        //computed properties
        //props
        //methods
        //lifecycles hooks
        //watchers
        login(){
            alert("bdhjsvfa")
        }
    }
</script>