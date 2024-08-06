#include <iostream>
#include <vector>
#include <string>

using namespace std;

class User {
private:
    int id;
    string name;
    string username;
    int score;

public:
    User(int id, const string& name, const string& username, int score)
        : id(id), name(name), username(username), score(score) {}

    int getId() const { return id; }
    string getName() const { return name; }
    string getUsername() const { return username; }
    int getScore() const { return score; }

    void setName(const string& newName) { name = newName; }
    void setUsername(const string& newUsername) { username = newUsername; }
    void setScore(int newScore) { score = newScore; }

    void display() const {
        cout << "ID: " << id << ", Name: " << name << ", Username: " << username << ", Score: " << score << '\n';
    }
};

void createUser(vector<User>& users) {
    int id;
    string name;
    string username;
    int score;

    cout << "Enter ID: ";
    cin >> id;
    cout << "Enter Name: ";
    cin.ignore();
    getline(cin, name);
    cout << "Enter Username: ";
    getline(cin, username);
    cout << "Enter Score: ";
    cin >> score;

    users.push_back(User(id, name, username, score));
    cout << "User created successfully.\n";
}

void readUser(const vector<User>& users) {
    if (users.empty()) {
        cout << "No users available.\n";
        return;
    }

    cout << "List of users:\n";
    for (const auto& user : users) {
        user.display();
    }
}

void updateUser(vector<User>& users) {
    if (users.empty()) {
        cout << "No users available.\n";
        return;
    }

    int id;
    cout << "Enter the ID of the user to update: ";
    cin >> id;

    for (auto& user : users) {
        if (user.getId() == id) {
            string newName, newUsername;
            cout << "Enter new Name: ";
            cin.ignore();
            getline(cin, newName);
            cout << "Enter new Username: ";
            getline(cin, newUsername);

            user.setName(newName);
            user.setUsername(newUsername);
            cout << "User updated successfully.\n";
            return;
        }
    }
    cout << "User not found.\n";
}

void deleteUser(vector<User>& users) {
    if (users.empty()) {
        cout << "No users available.\n";
        return;
    }

    int id;
    cout << "Enter the ID of the user to delete: ";
    cin >> id;

    for (auto it = users.begin(); it != users.end(); ++it) {
        if (it->getId() == id) {
            users.erase(it);
            cout << "User deleted successfully.\n";
            return;
        }
    }
    cout << "User not found.\n";
}

void manageScore(vector<User>& users) {
    if (users.empty()) {
        cout << "No users available.\n";
        return;
    }

    int id, newScore;
    cout << "Enter the ID of the user to manage score: ";
    cin >> id;

    for (auto& user : users) {
        if (user.getId() == id) {
            cout << "Current Score: " << user.getScore() << '\n';
            cout << "Enter new Score: ";
            cin >> newScore;
            user.setScore(newScore);
            cout << "Score updated successfully.\n";
            return;
        }
    }
    cout << "User not found.\n";
}

int main() {
    vector<User> users;
    bool exit = false;

    while (!exit) {
        cout << "===== Main Menu =====\n";
        cout << "1. Create User\n";
        cout << "2. Read Users\n";
        cout << "3. Update User\n";
        cout << "4. Delete User\n";
        cout << "5. Manage Score\n";
        cout << "6. Exit\n";
        cout << "Select an option: ";
        int choice;
        cin >> choice;

        switch (choice) {
        case 1:
            createUser(users);
            break;
        case 2:
            readUser(users);
            break;
        case 3:
            updateUser(users);
            break;
        case 4:
            deleteUser(users);
            break;
        case 5:
            manageScore(users);
            break;
        case 6:
            exit = true;
            break;
        default:
            cout << "Invalid option, please try again.\n";
        }
        cout << '\n';
    }

    return 0;
}
