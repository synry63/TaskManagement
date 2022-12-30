
export default {
    props: {
        callback: {
            type: Function
        },
        objToDelete:null
    },
    data() {
        return {
            errorText : ""
        }
    },
    created() {
        
    },
    methods: {
        errorHandling(data) {
            if (!('errors' in data) && data.id !=undefined) {
                this.callback(data);
            }
            else {
                this.errorText = "error 500, check network log tab. Probably on constraint on foreign key (Restrict)";
            }
        },
        suprime() {
            if (this.objToDelete instanceof Material) {
                api_DeleteMaterial(this.objToDelete.id, this.errorHandling);
            }
            else if (this.objToDelete instanceof Task) {
                api_DeleteTask(this.objToDelete.id, this.errorHandling);
            }
            
        }
    },
    template: `
               <a style="float:right" @click="suprime()" href="javascript:void(0);">delete</a>
               <p style="color:red;" v-if="errorText!=''">{{errorText}}</p>
              `
}