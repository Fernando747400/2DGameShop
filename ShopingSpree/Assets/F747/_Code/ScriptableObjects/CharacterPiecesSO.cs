using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Pieces List", menuName = "ShoopingSpree/Character Pieces List")]
public class CharacterPiecesSO : ScriptableObject
{
    public List<BaseItemSO> HoodPieces;
    public List<BaseItemSO> HairPieces;
    public List<BaseItemSO> FacePieces;
    public List<BaseItemSO> HeadPieces;
    public List<BaseItemSO> ShoulderPieces;
    public List<BaseItemSO> ElbowPieces;
    public List<BaseItemSO> TorsoPieces;
    public List<BaseItemSO> WristPieces;
    public List<BaseItemSO> PelvisPieces;
    public List<BaseItemSO> LegsPieces;
    public List<BaseItemSO> BootPieces;
    public List<BaseItemSO> WeaponPieces;

    public int HoodCurrentIndex = 0;
    public int HairCurrentIndex = 0;
    public int FaceCurrentIndex = 0;
    public int HeadCurrentIndex = 0;
    public int ShoulderCurrentIndex = 0;
    public int ElbowCurrentIndex = 0;
    public int TorsoCurrentIndex = 0;
    public int WristsCurrentIndex = 0;
    public int PelvisCurrentIndex = 0;
    public int LegsCurrentIndex = 0;
    public int BootCurrentIndex = 0;
    public int WeaponCurrentIndex = 0;

    private Dictionary<BodyPartType, List<BaseItemSO>> _piecesDictionary = new Dictionary<BodyPartType, List<BaseItemSO>>();
    private Dictionary<BodyPartType, int> _indexDictionary = new Dictionary<BodyPartType, int>();

    public List<BaseItemSO> GetListOf(BodyPartType partType)
    {
        if (_piecesDictionary.Count == 0)
        {
            BuildPiecesDictionary();
        }
        List<BaseItemSO> responseList;
        _piecesDictionary.TryGetValue(partType, out responseList);
        return responseList;
    }   

    public void UpdateIndexOf(BodyPartType bodyPart, int newIndex)
    {
        if(_piecesDictionary.Count == 0)
        {
            BuildIndexDictionary();
        }
        _indexDictionary[bodyPart] = newIndex;
        SaveInfo(bodyPart, newIndex);
    }

    public void SaveInfo(BodyPartType piece, int index)
    {
        PlayerPrefs.SetInt(piece.ToString(), index);
    }

    public void LoadInfo()
    {
        HoodCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Hood.ToString());
        HairCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Hair.ToString());
        FaceCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Face.ToString());
        HeadCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Head.ToString());
        ShoulderCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Shoulder.ToString());
        ElbowCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Elbow.ToString());
        TorsoCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Torso.ToString());
        WristsCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Wrist.ToString());
        PelvisCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Pelvis.ToString());
        LegsCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Leg.ToString());
        BootCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Boot.ToString());
        WeaponCurrentIndex = PlayerPrefs.GetInt(BodyPartType.Weapon.ToString());
    }

    private void BuildPiecesDictionary()
    {
        _piecesDictionary = new Dictionary<BodyPartType, List<BaseItemSO>>
        {
            { BodyPartType.Hood, HoodPieces },
            { BodyPartType.Hair, HairPieces },
            { BodyPartType.Face, FacePieces },
            { BodyPartType.Head, HeadPieces },
            { BodyPartType.Shoulder, ShoulderPieces },
            { BodyPartType.Elbow, ElbowPieces },
            { BodyPartType.Torso, TorsoPieces },
            { BodyPartType.Wrist, WristPieces },
            { BodyPartType.Pelvis, PelvisPieces },
            { BodyPartType.Leg, LegsPieces },
            { BodyPartType.Boot, BootPieces },
            { BodyPartType.Weapon, WeaponPieces }
        };
    }

    private void BuildIndexDictionary()
    {
        _indexDictionary = new Dictionary<BodyPartType, int>
        {
            { BodyPartType.Hood, HoodCurrentIndex },
            { BodyPartType.Hair, HairCurrentIndex },
            { BodyPartType.Face, FaceCurrentIndex },
            { BodyPartType.Head, HeadCurrentIndex },
            { BodyPartType.Shoulder, ShoulderCurrentIndex },
            { BodyPartType.Elbow, ElbowCurrentIndex },
            { BodyPartType.Torso, TorsoCurrentIndex },
            { BodyPartType.Wrist, WristsCurrentIndex },
            { BodyPartType.Pelvis, PelvisCurrentIndex },
            { BodyPartType.Leg, LegsCurrentIndex },
            { BodyPartType.Boot, BootCurrentIndex },
            { BodyPartType.Weapon, WeaponCurrentIndex }
        };
    }
}
