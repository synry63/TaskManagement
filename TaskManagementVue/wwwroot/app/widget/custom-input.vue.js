
export default {
    props: {
        name: String,
        valueInit: {
            type: [String, Number]
        },
        inputType: {
            type: String,
            default: "text"
        }
    },
    data() {
        return {
            value: this.valueInit
        }
    },
    created() {

    },
    methods: {
        change(event) {
            var key = event.target.name;
            var value = this.value;
            this.$emit('update', { key, value })
        }
    },
    watch: {
        valueInit() {
            this.value = this.valueInit;
        }
    },
    template: `
                <span v-if="inputType === 'hidden'">
                    <input type="hidden" v-model="value" v-bind:id="name" v-bind:name="name">
                </span>
                <span v-else>
                    <label v-bind:for="name">{{name}}</label>
                    <input  @input="change" :type="inputType" v-model="value" v-bind:id="name" v-bind:name="name">
                </span>
               `
}