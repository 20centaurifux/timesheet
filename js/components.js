Vue.component('taskTable',
{
	props: ['tasks', 'availableTasks', 'availableProjects'],
	template: taskTable,
});

Vue.component('taskRow',
{
	props: ['task', 'availableTasks', 'availableProjects'],
	data: function()
	{
		return {initialized: false}
	},
	template: taskRow,
	mounted: function()
	{
		// column 1 - timepicker:
		$(this.$el).find('input')
			.timepicker({showMeridian: false, maxHours: 12, defaultTime: '00:00 AM'}).val(this.task['hours'])
			.bind('change', this.notifyUpdate);

		// column 2 - task list:
		var source = '';

		for(var k in this.availableTasks)
		{
			source += '<option>' + k + '</option>';
		}

		$(this.$el).find('select:first')
			.addClass('selectpicker')
			.bind('change', this.taskChanged)
			.html(source)
			.val(this.task['task'])
			.selectpicker({liveSearch: true});

		// populate column 3 by calling taskChanged() event:
		this.taskChanged(true);
	},
	methods:
	{
		taskChanged: function(internal)
		{
			var el = $(this.$el).find('select:first').get(0);
			var taskName = $(el).val();
			var projectSource = this.availableTasks[taskName]['projects'];

			if(projectSource)
			{
				var child = document.createElement('select');

				$(child).addClass('selectpicker');
				$(child).html(this.availableProjects[projectSource].map(function(t) {return '<option>' + t + '</option>'}).join(''));
			}

			$(el).parents('tr').find('td:eq(2)').empty();

			if(child)
			{
				$(el).parents('tr').find('td:eq(2)').append(child);

				if($(child).is('select'))
				{
					$(child).val(this.task['project'])
						.selectpicker({liveSearch: true})
						.bind('change', this.notifyUpdate);
				}
			}

			if(this.initialized)
			{
				this.notifyUpdate();
			}
			else
			{
				this.initialized = true;
			}
		},
		notifyUpdate: function()
		{
			var task = {id: this.task.id,
				    hours: $(this.$el).find('input').val(),
				    task: $(this.$el).find('select:first').val(),
				    project: $(this.$el).find('select:eq(1)').val()};

			app.$emit('task-updated', task)
		}
	}
});

Vue.component('addTaskButton',
{
	template: '<button class="btn btn-primary center-block" @click="createTask()">Add</button>',
	methods:
	{
		createTask: function()
		{
			app.$emit('create-task');
		}
	}
});

Vue.component('removeTaskButton',
{
	props: ['task'],
	template: '<button class="btn btn-danger" @click="removeTask()">Remove</button>',
	methods:
	{
		removeTask: function()
		{
			app.$emit('task-removed', this.task);
		}
	}
});

Vue.component('datePicker',
{
	template: datePicker,
	mounted: function()
	{
		$(this.$el).find('.date:first')
			.datetimepicker({format: 'DD.MM.YYYY', defaultDate: new Date(), showTodayButton: true})
			.on('dp.change', function(e) { app.$emit('date-changed', e.date) });
	}
});
