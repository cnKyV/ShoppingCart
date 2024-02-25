cmake_minimum_required(VERSION 3.26)
project(ShoppingCart_Cpp)

set(CMAKE_CXX_STANDARD 17)

add_executable(ShoppingCart_Cpp main.cpp
        InputReceiver.hpp
        include/nlohmann/json.hpp
        include/nlohmann/json_fwd.hpp
        include/input-command-handler/command.hpp
        include/input-command-handler/command-invoker.hpp
        include/input-command-handler/add-item-command.hpp
)
