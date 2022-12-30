
export default {
    props: {
        unitMeasureId: {
            type: [String, Number],
            default: ""
        },
        disabled: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            units: [],
            selected: this.unitMeasureId
        }
    },
    watch:{
        unitMeasureId() {
            this.selected = this.unitMeasureId;
        }
    },
    created() {
        api_GetUnits(this.resultAPI);
    },
    methods: {
        resultAPI(data) {
            data.forEach((obj) => {
                this.units.push(new UnitOfIssue(obj));
            });
        },
        change(event) {
            var id = event.target.value;
            var unit = this.units.find(item => item.id == id);
            this.$emit('update', unit);
        }
    },
    template: ` 
                <label>unit</label>
                <select v-model="selected" :disabled="disabled"  @input="change">
                    <option  v-for="u in units" :value="u.id">{{u.name}}</option>
                </select>
               `
}