﻿using AutoMapper;
using Hero_net.DTO.Character;
using Hero_net.Service;

namespace Hero_net.Service.Imp;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character {Id = 1, Name = "Nam"}
    };

    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        return new ServiceResponse<List<GetCharacterDto>>
            {Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()};
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var character = characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        Character character = _mapper.Map<Character>(newCharacter);
        character.Id = characters.Max(c => c.Id) + 1;
        characters.Add(character);
        serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
        try
        {
            Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            // character.Name = updatedCharacter.Name;
            // character.HitPoints = updatedCharacter.HitPoints;
            // character.Strength = updatedCharacter.Strength;
            // character.Defense = updatedCharacter.Defense;
            // character.Intelligence = updatedCharacter.Intelligence;
            // character.Class = updatedCharacter.Class;
            _mapper.Map(updatedCharacter, character);

            response.Data = _mapper.Map<GetCharacterDto>(character);
        } catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

        try
        {
            Character character = characters.First(c => c.Id == id);
            characters.Remove(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}