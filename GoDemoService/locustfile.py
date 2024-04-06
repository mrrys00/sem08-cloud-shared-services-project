from locust import HttpUser, TaskSet, task, between

class UserBehavior(TaskSet):
    @task
    def get_alert(self):
        self.client.get("/alert")

    @task
    def get_hello(self):
        self.client.get("/hello")

class WebsiteUser(HttpUser):
    tasks = [UserBehavior]
    wait_time = between(1, 2)
