// Models
class UnitOfIssue {
    constructor(obj) {
        Object.assign(this, obj);
    }
    id;
    name;
    sort;
}
class Material {
    constructor(obj = undefined) {
        var targetKeys = Object.keys(this);
        var objFiltered = _.pick(obj, targetKeys);
        Object.assign(this, objFiltered);
        this.unitOfIssue = new UnitOfIssue(this.unitOfIssue);
    }
    id ="";
    partnumber = "";
    manufacturerCode = 0;
    price = 0;
    unitOfIssue = new UnitOfIssue();
}
class Task {
    constructor(obj = undefined) {
        var targetKeys = Object.keys(this);
        var objFiltered = _.pick(obj, targetKeys);
        Object.assign(this, objFiltered);
        this.material = new Material(this.material);
    }
    id = "";
    name = "";
    description = 0;
    totalDuration = 0;
    material = new Material();
}
class TaskMaterialUsage {
    constructor(obj = undefined) {
        var targetKeys = Object.keys(this);
        var objFiltered = _.pick(obj, targetKeys);
        Object.assign(this, objFiltered);
        this.unitOfMeasurement = new UnitOfIssue(this.unitOfMeasurement);
    }
    id = "";
    amount = 0;
    unitOfMeasurement = new UnitOfIssue();
}

// URLs
const URL_ROOT = 'https://localhost:7067/api/'; // <--- HERE SET THE URL OF THE API

const URL_UNITS = URL_ROOT + "UnitMeasures"
const URL_MATERIALS = URL_ROOT + "Materials"
const URL_MATERIAL = URL_ROOT + "Materials/"
const URL_TASKS = URL_ROOT + "Tasks"
const URL_TASK = URL_ROOT + "Tasks/"
const URL_TASK_MATERIAL_USAGE = URL_ROOT + "TaskMaterialUsages/"
const URL_TASK_MATERIAL_USAGES = URL_ROOT + "TaskMaterialUsages"

// GET UnitOfIssue
function api_GetUnits(callback) {
    var url = URL_UNITS;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// GET TaskMaterialUsage
function api_GetTaskMaterialUsage(idTask,idMaterial, callback) {
    var url = URL_TASK_MATERIAL_USAGE + idTask + "/" + idMaterial;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// SET TaskMaterialUsage
function api_SetTaskMaterialUsage(taskMaterialUsage, callback) {
    var url = URL_TASK_MATERIAL_USAGES;
    var requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(taskMaterialUsage)
    };
    fetch(url, requestOptions)
        .then(response => response.json())
        .then(data => callback(data));
}
// GET Task
function api_GetTask(id, callback) {
    var url = URL_TASK + id;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// GET Tasks
function api_GetTasks(callback) {
    var url = URL_TASKS;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// SET Task
function api_SetTask(task, callback) {
    var url = URL_TASKS;
    var requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(task)
    };
    fetch(url, requestOptions)
        .then(response => response.json())
        .then(data => callback(data));
}
// DELETE Task
function api_DeleteTask(id, callback) {
    var url = URL_TASK + id;
    fetch(url, { method: 'DELETE' })
        .then(response => response.json())
        .then(data => callback(data))
        .catch(function (error) {
            callback(error);
        });
}
// GET Material
function api_GetMaterial(id,callback) {
    var url = URL_MATERIAL + id;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// GET Materials
function api_GetMaterials(callback) {
    var url = URL_MATERIALS;
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}
// SET Material
function api_SetMaterial(material, callback) {
    var url = URL_MATERIALS;
    var requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(material)  
    };
    fetch(url, requestOptions)
        .then(response => response.json())
        .then(data => callback(data));
}
// DELETE Material
function api_DeleteMaterial(id, callback) {
    var url = URL_MATERIAL + id;
    fetch(url, { method: 'DELETE' })
        .then(response => response.json())
        .then(data => callback(data))
        .catch(function (error) {
            callback(error);
        });
}