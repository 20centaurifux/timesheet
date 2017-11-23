var TimesheetREST = function(options)
{
	this.opts = $.extend({Server: "http://localhost:5000", Username: "", Password: ""}, options);
};

TimesheetREST.prototype.setAuthData = function(username, password)
{
	this.opts.Username = username;
	this.opts.Password = password;
}

TimesheetREST.prototype.getProjects = function()
{
	return this.requestJSON("GET", "/projects");
}

TimesheetREST.prototype.getTasks = function()
{
	return this.requestJSON("GET", "/tasks");
}

TimesheetREST.prototype.createEntry = function(date, hours, task, project)
{
	var e = {hours: hours, task: task, project: project};

	return this.requestJSON("PUT", "/timesheet/" + encodeURIComponent(this.opts.Username) + "/" + date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate(), e); 
}

TimesheetREST.prototype.updateEntry = function(entry)
{
	return this.requestJSON("POST", "/timesheet/" + encodeURIComponent(this.opts.Username) + "/entries/" + entry.id, entry);
}

TimesheetREST.prototype.deleteEntry = function(id)
{
	return this.requestJSON("DELETE", "/timesheet/" + encodeURIComponent(this.opts.Username) + "/entries/" + id);
}

TimesheetREST.prototype.getEntries = function(dt, hours, task, project)
{
	return this.requestJSON("GET", "/timesheet/" + encodeURIComponent(this.opts.Username) + "/" + dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate());
}

TimesheetREST.prototype.request = function(method, type, path, data)
{
	return $.ajax(
	{
		type: method,
		url: this.opts.Server + path,
		dataType: type,
		async: true,
		headers: {"Authorization": "Basic " + btoa(this.opts.Username + ":" + this.opts.Password), "Content-Type": "application/json"},
		data: JSON.stringify(data)
	});
}

TimesheetREST.prototype.requestJSON = function(method, path, data)
{
	return this.request(method, "json", path, data);
}
