// Display and events of list of "Materials" model. One as links, and one as dropdown
import material from '/app/material/material.vue.js'
import customDelete from '/app/widget/custom-delete.vue.js'
export default {
    props: {
        materialId: {
            type: String,
            default: ""
        },
        isInput: {
            type: Boolean,
            default: false
        },
    },
    components: {
        material,
        customDelete
    },
    data() {
        return {
            materials: [],
            material_item: null,
            materialIdSelected : this.materialId
        }
    },
    methods: {
        deleteItemFromArray(mat) {
            this.unSelected();
            var index = this.materials.findIndex(item => item.id == mat.id);
            this.materials.splice(index, 1);
        },
        addOrUpdateItemFromArray(mat) {
            this.unSelected();
            var index = this.materials.findIndex(item => item.id == mat.id);
            if (index != -1) {
                this.materials[index] = new Material(mat);
            }
            else {
                this.materials.push(new Material(mat));
            }

        },
        resultAPI(data) {
            data.forEach((obj) => {
                this.materials.push(new Material(obj));
            });
        },
        selected(id) {
            this.unSelected();
            this.material_item = this.materials.find(item => item.id == id);
            this.material_item.show = true;
        },
        unSelected() {
            this.materials.forEach(item => item.show = false);
        },
        resultAPIdelete(data) {
            this.refresh(data, "delete");
            
        },
        change(event) {
            var id = event.target.value;
            var material = this.materials.find(item => item.id == id);
            this.$emit('update', material);
        }

    },
    computed: {
        materialComponent() {

            if (this.material_item) {
                return material;
            }
        }
    },
    created() {
        api_GetMaterials(this.resultAPI)
    },
    mounted() {

    },
    template: `
                <div v-if="isInput">
                    <label>material</label>
                    <select v-model="materialIdSelected" @input="change" name="materials" id="materials">
                        <option  v-for="mat in materials" :value="mat.id">{{mat.partnumber}}</option>
                    </select>
                </div>    
                <ul v-else>
                    <h5>MATERIALS</h5>
                    <li  style="width:500px;" v-for="mat in materials">
                        <a v-on:click="selected(mat.id)" href="javascript:void(0);">{{mat.partnumber}}</a>
                        <custom-delete :obj-to-delete="mat" :callback="deleteItemFromArray"></custom-delete>
                        <material @refresh="addOrUpdateItemFromArray" :disabled-unit="true" v-if="mat.show" :id-material="mat.id" />
                    </li>
               </ul>`
}