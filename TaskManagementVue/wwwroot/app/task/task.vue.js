// display form of one "Task" model. For edit/create.
import customInput from '/app/widget/custom-input.vue.js'
import materials from '/app/material/materials.vue.js'
import customSubmit from '/app/widget/custom-submit.vue.js'
import taskMaterialUsage from '/app/task_material_usage/task_material_usage.vue.js'
export default {
    props: {
            idTask: {
            type: String,
            default: ""
        },
    },
    emits: {

    },
    components: {
        customInput,
        materials,
        customSubmit,
        taskMaterialUsage
    },
    data() {
        return {
            task: null,
            tmu:null,
        }
    },
    created() {
        if (this.idTask == "") {
            this.task = new Task();
            this.tmu = new TaskMaterialUsage();
            this.tmu.task = this.task;
        }
        else api_GetTask(this.idTask, this.resultTaskAPI);

        
    },
    mounted() {
       
    },
    computed: {
        
    },
    methods: {
        resultAPIpersist(data) {
            window.location.hash = "/tasks"
            this.$emit('refresh', data);
        },
        resultTaskAPI(data) {
            this.task = new Task(data);
            this.tmu = new TaskMaterialUsage();
            this.tmu.task = this.task;
        },
        isMaterial(value) {
            if (value instanceof Material) {
                return true;
            }
            else return false;

        },
        getInputType(key,value) {
            if (typeof value === 'number') {
                return "number";
            }
            else if (validator.isUUID(value) || key == "id") {
                return "hidden"
            }
            else if (value=="") {
                return "text";
            }

        },
        customInputChange(obj) {
            this.task[obj.key] = obj.value;
        },
        materialsChange(obj) {
            this.task.material = new Material(obj);
        },
        taskMaterialUsageChange(obj) {
            this.tmu = new TaskMaterialUsage(obj);
            this.tmu.task = this.task;
            this.tmu.material = this.task.material;
            

        }
    },
    template: `<ul>
                    <h5>CREATE TASK</h5>
                    <li v-for="(value, key) in task">
                        <materials @update="materialsChange" v-if="isMaterial(value)" :is-input="true" :material-id="value.id"></materials>
                        <custom-input v-else
                            :name="key"
                            :value-init="value"
                            :input-type="getInputType(key,value)"
                            @update="customInputChange"
                        ></custom-input>
                    </li>
                    <ul>
                        <task-material-usage @update="taskMaterialUsageChange" v-if="task!=null" :task="task"></task-material-usage>
                    </ul>
                   <custom-submit :obj-to-persist="tmu" :callback="resultAPIpersist"></custom-submit>
                    
               </ul>`
}