#include <iostream>
#include "include/nlohmann/json.hpp"

using json = nlohmann::json;
int main() {
    auto json_string = R"({"command":"addItem","payload":{"itemId":32,"categoryId":32,"sellerId":32,"price":32,"quantity":1}} )";

    json j = json::parse(json_string);

    std::cout << j["command"];
    return 0;
}
