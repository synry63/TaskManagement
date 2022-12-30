import task from '/app/task/task.vue.js'
import customDelete from '/app/widget/custom-delete.vue.js'
export default {
    props: {
    },
    components: {
        task,
        customDelete
    },
    data() {
        return {
            tasks_arr: [],
            task_item: null
        }
    },
    methods: {
        deleteItemFromArray(task) {
            this.unSelected();
            var index = this.tasks_arr.findIndex(item => item.id == task.id);
            this.tasks_arr.splice(index, 1);
        },
        addOrUpdateItemFromArray(tm) {
            this.unSelected();
            var index = this.tasks_arr.findIndex(item => item.id == tm.task.id);
            if (index != -1) {
                this.tasks_arr[index] = new Task(tm.task);
            }
            else {
                this.tasks_arr.push(new Task(tm.task));
            }

        },
        resultTasksAPI(data) {
            data.forEach((obj) => {
                this.tasks_arr.push(new Task(obj));
            });


        },
        selected(id) {
            this.unSelected();
            this.task_item = this.tasks_arr.find(item => item.id == id);
            this.task_item.show = true;
        },
        unSelected() {
            this.tasks_arr.forEach(item => item.show = false);
        },
        resultAPIdelete(data) {
            this.refresh(data, "delete");
            
        },

    },
    computed: {

    },
    created() {
        api_GetTasks(this.resultTasksAPI)
    },
    mounted() {

    },
    template: `
                    <h5>TASKS</h5>
                <ul>
                    <li  style="width:500px;" v-for="task in tasks_arr">
                        <a v-on:click="selected(task.id)" href="javascript:void(0);">{{task.name}}</a>
                        <custom-delete :obj-to-delete="task" :callback="deleteItemFromArray"></custom-delete>
                        <task @refresh="addOrUpdateItemFromArray" v-if="task.show" :id-task="task.id"></task>
                    </li>
               </ul>
`
}