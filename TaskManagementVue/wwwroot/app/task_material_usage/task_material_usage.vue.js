// Display and events of "TaskMatarielUsage" Models.
import customInput from '/app/widget/custom-input.vue.js'
import units from '/app/widget/units.vue.js'
export default {
    props: {
            task: {
            type: Object
        },
    },
    components: {
        customInput,
        units
    },
    data() {
        return {
            taskMaterialUsage : null,
        }
    },
    created() {
        this.init();
    },
    watch: {
        'task.material': function (val, oldVal) {
            this.init();
        }
    },
    mounted() {
        
    },
    computed: {

    },
    methods: {
        init() {
            if (this.task.id!="" && this.task.material.id != "") {
                api_GetTaskMaterialUsage(this.task.id, this.task.material.id, this.resultTaskAPI);
            }
            else {
                this.taskMaterialUsage = new TaskMaterialUsage();
                    
            }
            
        },
        emitUpdate() {
            this.$emit('update', this.taskMaterialUsage);
        },
        resultTaskAPI(data) {
            if (data.hasOwnProperty('id') && validator.isUUID(data.id)) {
                this.taskMaterialUsage = new TaskMaterialUsage(data);
            }
            else {
                this.taskMaterialUsage = new TaskMaterialUsage();
            }
            this.emitUpdate();
        },
        getInputType(key,value) {
            if (typeof value === 'number') {
                return "number";
            }
            else if (validator.isUUID(value) || key=="id") {
                return "hidden"
            }
            else if (value == "") {
                return "text";
            }

        },
        customInputChange(obj) {
            this.taskMaterialUsage[obj.key] = obj.value;
            this.emitUpdate();
        },
        unitsChange(obj) {
            this.taskMaterialUsage.unitOfMeasurement = new UnitOfIssue(obj);
            this.emitUpdate();

        },
        isUnitOfIssue(value) {
            if (value instanceof UnitOfIssue) {
                return true;
            }
            else return false;

        }

    },
    template: `
                 <li v-for="(value, key) in taskMaterialUsage">
                        <units @update="unitsChange" v-if="isUnitOfIssue(value)" :unit-measure-id="value.id"></units>
                        <custom-input v-else
                            :name="key"
                            :value-init="value"
                            :input-type="getInputType(key,value)"
                            @update="customInputChange"
                        ></custom-input>    
                </li>
              `
}