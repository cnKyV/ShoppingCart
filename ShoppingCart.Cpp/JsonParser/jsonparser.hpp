//
// Created by cenka on 11/25/2023.
//

#ifndef SHOPPINGCART_CPP_JSONPARSER_HPP
#define SHOPPINGCART_CPP_JSONPARSER_HPP

#define OPENING_BRACKET_ASCII 123
#define CLOSING_BRACKET_ASCII 125
#define QUOTE_ASCII 34
#define COLON_ASCII 58


#include <iostream>
#include <map>

class jsonParser
{
public:

    jsonParser(std::string input)
    {
        _input = std::move(input);
    }

    template<typename T>
    T parseJsonToObj(std::string& jsonAsString)
    {
        std::map<jsonData, jsonData>* deconstructedKeyValuePairs = new std::map<jsonData, jsonData>();

        if (jsonAsString[0] != '{')
            throw std::runtime_error("The string provided is not json.");




    }

    template<typename T>
    void parseJsonToObj(T& obj)
    {


    }

private:
    static std::string _input;

    struct jsonData{
        std::string path;
        size_t indent;
        jsonData* parent;
        std::map<std::string, std::string> keyValuePair;
    };

    template<typename T>
    std::map<jsonData, jsonData>* asciiMatcher(T& obj, std::string input = _input)
    {
        bool _isFinished = false;
        bool _isReadingOpen = false;
        bool _isInsideQuote = false;
        size_t _indent = 0;
        size_t _index = 0;



        while(!_isFinished)
        {
            wchar_t word = input[_index];


            switch (word) {
                case OPENING_BRACKET_ASCII:
                    _isReadingOpen = true;
                    break;
                case CLOSING_BRACKET_ASCII:
                    _isReadingOpen = false;
                    break;
                case QUOTE_ASCII:
                    if (!_isInsideQuote)
                    {
                        _isInsideQuote = true;
                    }
                    else
                    {
                        _isInsideQuote = false;
                    }
                    break;
                default:



            }


            ++_index;
        }






    }




};

#endif //SHOPPINGCART_CPP_JSONPARSER_HPP
