
import customInput from '/app/widget/custom-input.vue.js'
import units from '/app/widget/units.vue.js'
import customSubmit from '/app/widget/custom-submit.vue.js'
export default {
    props: {
            idMaterial: {
            type: String,
            default: ""
        },
        disabledUnit: {
            type: Boolean,
            default: false
        },
    },
    emits: {

    },
    components: {
        customInput,
        units,
        customSubmit
    },
    data() {
        return {
            material: undefined,
        }
    },
    created() {
        if (this.idMaterial == "") {
            this.material = new Material();
        }
        else api_GetMaterial(this.idMaterial, this.resultAPI);
    },
    mounted() {
       
    },
    computed: {
        
    },
    methods: {
        resultAPIpersist(data) {
            window.location.hash = "/materials"
            this.$emit('refresh', data);
        },
        resultAPI(data) {
            this.material = new Material(data);
        },
        isUnitOfIssue(value) {
            if (value instanceof UnitOfIssue) {
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
            this.material[obj.key] = obj.value;
        },
        unitsChange(obj) {
            this.material.unitOfIssue = new UnitOfIssue(obj);
        }
    },
    template: `<ul>
                    <h5>CREATE MATERIAL</h5>
                    <li v-for="(value, key) in material">
                        <units @update="unitsChange" v-if="isUnitOfIssue(value)" :disabled="disabledUnit" :unit-measure-id="value.id"></units>
                        <custom-input v-else
                            :name="key"
                            :value-init="value"
                            :input-type="getInputType(key,value)"
                            @update="customInputChange"
                        ></custom-input>
                    </li>
                   <custom-submit :obj-to-persist="material" :callback="resultAPIpersist"></custom-submit>
                    
               </ul>`
}