//
// Created by cenka on 2/25/2024.
//

#ifndef SHOPPINGCART_CPP_COMMAND_INVOKER_HPP
#define SHOPPINGCART_CPP_COMMAND_INVOKER_HPP

#include "command.hpp"
#include "../nlohmann/json.hpp"
#include <variant>

using json = nlohmann::json;



class CommandInvoker {
private:
    std::unordered_map<std::string, std::shared_ptr<Command>> commands;

    json inputReceiver(const std::string& input){
        auto result = json::parse(input);

        auto command = result["command"];



    }

    std::unordered_map<std::string, std::variant<std::string, int, double>> receivePayload(const json& keyValuePairs)
    {
        for(const auto& [key, value] : keyValuePairs.items())
        {

        }
    }



public:



    void registerCommand(const std::string& name,std::shared_ptr<Command> command){
        commands[name] = command;
    }

    void executeCommand(const std::string& name){
        if (commands.find(name) != commands.end())
        {
            commands[name]->execute();
        } else {
            std::cout << "Error: Command '" << name << "' not found." << std::endl;
        }
    }
};
#endif //SHOPPINGCART_CPP_COMMAND_INVOKER_HPP
