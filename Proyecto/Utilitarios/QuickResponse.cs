﻿using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class QuickResponse
{
    /// <summary>
    /// Método que devuelve un la imagen generada
    /// El primer parámetro es la palabra(s) a convertir
    /// y el segundo parámetro es el nivel. Este parámetro  es muy importante
    /// </summary>
    /// <param name="input"></param>
    /// <param name="qrlevel"></param>
    /// <returns></returns>
    public static Image QuickResponseGenerador(string input, int qrlevel)
    {

        string toenc = input;

        QRCodeEncoder qe = new QRCodeEncoder();

        qe.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

        qe.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L; // - Using LOW for more storage

        qe.QRCodeVersion = qrlevel;

        Bitmap bm = qe.Encode(toenc);

        return bm;

    }


}

