
export default {
    props: {
        callback: {
            type: Function
        },
        objToPersist:null
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
            if (!('errors' in data)) {
                this.callback(data);
            }
            else {
                this.errorText = data;
            }
        },
        persist() {
            if (this.objToPersist.id == "") {
                delete this.objToPersist.id;
            }

            if (this.objToPersist instanceof Material) {
                api_SetMaterial(this.objToPersist, this.errorHandling);
            }
            else if (this.objToPersist instanceof TaskMaterialUsage) {
                if (this.objToPersist.task.id == "") {
                    delete this.objToPersist.task.id;
                }
                api_SetTaskMaterialUsage(this.objToPersist, this.errorHandling);
            }
            
        },
    },
    template: `
                <p><button @click="persist()">Save</button></p>
                <p style="color:red;" v-if="errorText!=''">{{errorText}}</p>
              `
}