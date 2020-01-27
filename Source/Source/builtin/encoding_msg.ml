(** Encoding Strings *)

open Builtin
open Basic_arithmetics
open Power

(** Encode a string containing ASCII characters.
    @param str is a string representing message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
 *)
let encode str bits =
  let lg = String.length str in
  let rec encodb str lg i a res = if lg = 0 then res else
      let scal = power 2 bits in
      encodb str (lg-1) (i-1) (scal * a) (Char.code str.[i]*a+res)
        in encodb str lg (lg-1) 1 0;;

(** Decode a string containing ASCII characters.
    @param msg is an integer representing an encoded message.
    @param bits number of bits on which to store a character ;
           alphanumeric ASCII is 7.
 *)
let decode msg bits =
  let scal = power 2 bits in
  let rec decodb q r scal u =
    let (s,t) = div q scal in if q = 0 then (Char.escaped(Char.chr r))^u else
        decodb s t scal ((Char.escaped(Char.chr r))^u) in
  let (q,r) = div msg scal in
  decodb q r scal "";;
