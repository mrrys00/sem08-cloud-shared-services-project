from locust import HttpUser, TaskSet, task, between

class UserBehavior(TaskSet):
    @task
    def get_transaction(self):
        self.client.get("/transaction")

    @task
    def get_balance(self):
        self.client.get("/balance")

class WebsiteUser(HttpUser):
    tasks = [UserBehavior]
    wait_time = between(1, 2) # Adjust wait time as per your requirement