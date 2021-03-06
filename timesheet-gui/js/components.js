Vue.component('taskTable',
{
	props: ['tasks', 'availableTasks', 'availableProjects'],
	template: '#taskTable'
});

Vue.component('taskRow',
{
	props: ['task', 'availableTasks', 'availableProjects'],
	data: function()
	{
		return {initialized: false}
	},
	template: '#taskRow',
	mounted: function()
	{
		// column 1 - timepicker:
		$(this.$el).find('input')
			.timepicker({showMeridian: false, showSeconds: true, maxHours: 12, defaultTime: this.task.hours + ' AM'}).val(this.task['hours'])
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
	watch:
	{
		task:
		{
			handler: function(val, oldVal)
			{
				$(this.$el).find('input').val(val['hours']);
				$(this.$el).find('select:first').val(val['task']);

				if(val['project'])
				{
					$(this.$el).find('select:eq(1)').val(val['project']);
				}
			},
			deep: true
		}
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

Vue.component('timerButton',
{
	props: ['task', 'interval'],
	data: function()
	{
		return {started: false, intervalId: null}
	},
	template: '<button class="btn btn-default" data-interval-id="" @click="toggleTimer()">Start Timer</button>',
	methods:
	{
		toggleTimer: function()
		{
			if(this.started)
			{
				clearInterval(this.intervalId);

				$(this.$el).text('Start Timer');
				this.started = false;
			}
			else
			{
				var taskId = this.task.id;
				var interval = this.interval;

				this.intervalId = setInterval(function()
				{
					app.$emit('tick', taskId, interval);
				}, interval * 1000);

				$(this.$el).text('Stop Timer');
				this.started = true;
			}

			$(this.$el).toggleClass('btn-info btn-default');
		}
	},
	beforeDestroy: function()
	{
		if(this.started)
		{
			clearInterval(this.intervalId);
		}
	}
});

Vue.component('datePicker',
{
	template: '#datePicker',
	mounted: function()
	{
		$(this.$el).find('.date:first')
			.datetimepicker({format: 'DD.MM.YYYY', defaultDate: new Date(), showTodayButton: true})
			.on('dp.change', function(e) { app.$emit('date-changed', e.date.toDate()) });
	}
});

Vue.component('productivity',
{
	props: ['availableTasks', 'tasks'],
	template: '<p><strong>Efficiency:</strong> {{calculateProducivity()}}%</p>',
	methods:
	{
		calculateProducivity()
		{
			var self = this;
			var total = 0;
			var productive = 0;
			var e = 0;

			this.tasks.forEach(function(e)
			{
				var m = e.hours.match(/(\d{1,2}):(\d{1,2}):(\d{1,2})/);
				var seconds = parseInt(m[1], 10) * 60 * 60 + parseInt(m[2], 10) * 60 + parseInt(m[3], 10);

				total += seconds;

				if(self.availableTasks[e.task].productive)
				{
					productive += seconds;
				}
			});

			if(total)
			{
				e = Math.floor(productive / total * 100);
			}

			return e;
		}
	}
});

Vue.component('authDialog',
{
	props: ['username', 'password'],
	template: '#authDialog',
	methods:
	{
		updateCredentials()
		{
			app.$emit('update-credentials',
				  {username: $(this.$el).find('input:eq(0)').val(),
				   password: $(this.$el).find('input:eq(1)').val()});
			$(this.$el).modal('hide');
		}
	}
});
