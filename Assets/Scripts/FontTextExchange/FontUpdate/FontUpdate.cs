using TMPro;
using System.Linq;  
using UnityEngine;

public class ChineseFix : MonoBehaviour
{
    [SerializeField] TMP_FontAsset dynFont;

    void Start()
    {
        string wholeText = GetComponent<TMP_Text>().text;
        uint[] codePoints = wholeText
            .ToCharArray()
            .Select(c => (uint)c)
            .ToArray();

        dynFont.TryAddCharacters(codePoints, out uint[] missing);

        if (missing.Length > 0)
        {
            string chars = string.Concat(missing.Select(cp => char.ConvertFromUtf32((int)cp)));
        }

        const string common3500 = "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着确才快完成挑" +
            "等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么故事发个西方镇这里原着平静活忽然有群不速之客突找上来他们占了居赖以存栖息下辟出座异建筑人苦堪言头戴工帽勇士" +
            "利比或但质气第向道命此变条只没结解问意建月公系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见" +
            "计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己置再转更" +
            "单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究越接案银商兵石目轮交立击区除去请项罗坝李...";

        bool ok = dynFont.TryAddCharacters(common3500, out var stillMissing);
        Debug.Log($"补字结果:{ok}, 仍缺:{stillMissing.Length}");
        GetComponent<TMP_Text>().SetAllDirty();  
    }
}